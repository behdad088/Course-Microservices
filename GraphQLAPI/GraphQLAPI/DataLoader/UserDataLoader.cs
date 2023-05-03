using FirebaseAdmin;
using FirebaseAdmin.Auth;
using GraphQLAPI.Schema.Queries.Users;

namespace GraphQLAPI.DataLoader
{
    public class UserDataLoader : BatchDataLoader<string, UserType>
    {
        private const int MAX_FIREBASE_BATCH_SIZE = 100;
        private readonly FirebaseAuth _firebaseAuth;
        public UserDataLoader(
            FirebaseApp firebaseApp,
            IBatchScheduler batchScheduler) : base(batchScheduler, new DataLoaderOptions()
            {
                MaxBatchSize = MAX_FIREBASE_BATCH_SIZE,
            })
        {
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        protected override async Task<IReadOnlyDictionary<string, UserType>> LoadBatchAsync(
            IReadOnlyList<string> userIds, CancellationToken cancellationToken)
        {
            var userIdentifiers = userIds.Select(x => new UidIdentifier(x)).ToList();
            var userResults = await _firebaseAuth.GetUsersAsync(userIdentifiers, new CancellationToken());
            var result = userResults.Users.Select(x => new UserType() { Id = x.Uid, Username = x.DisplayName }).ToDictionary(x => x.Id);

            return result;
        }
    }
}
