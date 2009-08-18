/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Apache.NMS.ActiveMQ.Commands;

namespace Apache.NMS.ActiveMQ.Transport.Mock
{    
    /// <summary>
    /// Builds responses using the internal Cononical OpenWire Commands format.
    /// </summary>
    public class OpenWireResponseBuilder : IResponseBuilder
    {
        public Response BuildResponse(Command command)
        {
            if( command.ResponseRequired ) 
            {
                // These Commands just require a response that matches their command IDs
                Response response = new Response();
                response.CorrelationId = command.CommandId;
                return response;
            }
            
            return null;
        }

        public List<Command> BuildIncomingCommands(Command command)
        {
            List<Command> commands = new List<Command>();
            
            // Delegate this to buildResponse
            if( command.ResponseRequired ) 
            {
                commands.Add( this.BuildResponse( command ) );
            }
        
            if( command.IsWireFormatInfo ) 
            {
                // Return a copy of the callers own requested WireFormatInfo
                // so they get exactly the settings they asked for.
                commands.Add( (Command) command.Clone() );
            }
            
            return commands;
        }              
    }
}