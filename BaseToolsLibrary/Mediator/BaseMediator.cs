﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace BaseToolsLibrary.Mediator
{
    /// <summary>
    ///     Base implementation of Mediator.
    ///     Will do the lookup for all IMediatorHandler and IMediatorCommand on construction
    /// </summary>
    public class BaseMediator : IMediator
    {
        public void RegisterHandler(IMediatorHandler handler, Type command)
        {
            if (_handlerCommandBinding.ContainsKey(command))
            {
                if (_handlerCommandBinding[command] != null)
                    Trace.WriteLine($"WARNING : registering a new handler for the command type '{command}' that already had one");
                _handlerCommandBinding[command] = handler;
            }
            else
            {
                if (ignore_broken_pairs)
                    Trace.WriteLine($"ERROR : trying to register a handler for the command type '{command.FullName}' that was not found by {this.GetType()}. This will be ignored." +
                                    "Set the ignore_broken_pairs constructor parameter to true (false by default) to treat this as an error");
                else
                {
                    Trace.WriteLine($"FATAL : trying to register a handler for the command type '{command.FullName}' that was not found by {this.GetType()}. This will be treated as an error." +
                                    "Set the ignore_broken_pairs constructor parameter to false (default) to ignore this error.");
                    throw new NullReferenceException($"trying to register a handler for the command type '{command.FullName}' that was not found by {this.GetType()}");
                }
            }
        }

        private void gather_handlers()
        {
            Trace.WriteLine("INFO : MediatorBase is scanning application for handlers");
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Trace.WriteLine($"DEBUG : MediatorBase is scanning '{assembly.FullName}' assembly looking for handlers");
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (false == type.GetTypeInfo().IsAbstract)
                        {
                            int i = 0;
                            foreach (Type _interface in type.GetInterfaces())
                            {
                                if (_interface.IsGenericType)
                                {
                                    if (_interface.GetGenericTypeDefinition() == typeof(IMediatorHandler<,>))
                                    {
                                        Trace.WriteLine($"==>INFO : MediatorBase found the '{type.FullName}' handler, its command type is {type.GetInterfaces()[i].GetGenericArguments()[0]}.");
                                        RegisterHandler(Activator.CreateInstance(type) as IMediatorHandler, type.GetInterfaces()[i].GetGenericArguments()[0]);
                                        break;
                                    }
                                }
                                i += 1;
                            }
                        }
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    Console.WriteLine($"WARNING : BaseMediator could not load types from {assembly.FullName}");
                }
            }
        }

        private Dictionary<Type, IMediatorHandler> _handlerCommandBinding = new Dictionary<Type, IMediatorHandler>();

        private void register_command(Type type)
        {
            if (_handlerCommandBinding.ContainsKey(type))
            {
                Console.WriteLine($"WARNING: registering a command that was already known in BaseMediator: {type.FullName}");
            }
            else
            {
                _handlerCommandBinding.Add(type, null);
            }
        }

        private void gather_commands()
        {
            Console.WriteLine("INFO : MediatorBase is scanning application for commands");
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine($"DEBUG : MediatorBase is scanning '{assembly.FullName}' assembly looking for commands");
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (false == type.GetTypeInfo().IsAbstract)
                        {
                            foreach (Type _interface in type.GetInterfaces())
                            {
                                if (_interface == typeof(IMediatorCommand))
                                {
                                    Console.WriteLine($"==>INFO : MediatorBase found the '{type.FullName}' command.");
                                    register_command(type);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    Console.WriteLine($"WARNING : BaseMediator could not load types from {assembly.FullName}");
                }
            }
        }

        private void sanity_check()
        {
            int errors = 0;
            Console.WriteLine("INFO : MediatorBase is looking for any unhandled command");

            foreach (KeyValuePair<Type, IMediatorHandler> pair in _handlerCommandBinding)
            {
                if (pair.Value == null)
                {
                    Console.WriteLine($"ERROR : MediatorBase did not find a corresponding handler to the command '{pair.Key.FullName}'");
                    errors += 1;
                }
            }

            if (errors != 0)
            {
                if (ignore_broken_pairs)
                {
                    Console.WriteLine($"ERROR : MediatorBase is missing {errors} handlers for given commands, this will be ignored for now. " +
                        "Set the ignore_broken_pairs constructor parameter to false (default) to treat this as an error");
                }
                else
                {
                    Console.WriteLine($"FATAL : MediatorBase is missing {errors} handlers for given commands, it will be treated as an error. " +
                        "Set the ignore_broken_pairs constructor parameter to true (false by default) to treat this as an error");
                    throw new NullReferenceException($"MediatorBase is missing {errors} handlers for given commands.");
                }
            }
            else
                Console.WriteLine("INFO : MediatorBase did not find any unhandled command.");
        }

        private bool ignore_broken_pairs = false;

        /// <summary>
        ///     Will throw if any handler is missing for a given command, and if the ignore_missing_handler is not set to true
        /// </summary>
        /// <param name="ignore_missing_handlers"></param>
        public BaseMediator(bool ignore_broken_pairs = false)
        {
            this.ignore_broken_pairs = ignore_broken_pairs;
            gather_commands();
            gather_handlers();
            sanity_check();
        }

        protected IMediatorHandler get_handler_from_command(IMediatorCommand command)
        {
            KeyValuePair<Type, IMediatorHandler> pair = _handlerCommandBinding.FirstOrDefault(x => x.Key == command.GetType());

            if (pair.Key == null)
            {
                string message = $"ERROR : trying to handle a command that was not detected by BaseMediator: \"{command.GetType()}\"";
                Console.WriteLine(message);
                throw new NullReferenceException(message);
            }
            if (pair.Value == null)
            {
                string message = $"ERROR : trying to handle a command that has no corresponding Handler in BaseMediator: \"{command.GetType()}\"";
                Console.WriteLine(message);
                throw new NullReferenceException(message);
            }
            return pair.Value;
        }

        public IMediatorCommandResponse Execute(IMediatorCommand command)
        {
            IMediatorHandler handler = get_handler_from_command(command);

            Trace.WriteLine($"executing command {command.GetType()}");

            return handler.Execute(command);
        }

        public void Undo(IMediatorCommand command)
        {
            IMediatorHandler handler = get_handler_from_command(command);

            Console.WriteLine($"undoing command {command.GetType()}");

            handler.Undo(command);
        }
    }
}
