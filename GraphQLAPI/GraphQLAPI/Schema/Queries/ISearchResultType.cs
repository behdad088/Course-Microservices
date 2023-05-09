namespace GraphQLAPI.Schema.Queries
{
    //[UnionType("SearchResult")] The same as InterfaceType but we use it when we have not share property. Id is a shared property in InterfaceType
    [InterfaceType("SearchResult")]
    public interface ISearchResultType
    {
        Guid Id { get; }
    }
}
