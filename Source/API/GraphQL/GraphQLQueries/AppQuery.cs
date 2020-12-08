using API.GraphQLTypes;
using GraphQL.Types;

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
