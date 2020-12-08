using API.GraphQLTypes;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IMessageRepository repository)
        {
            Field<ListGraphType<MessageType>>(
               "messages",
               resolve: context => repository.GetMessage()
           );
        }
    }
}
