using ContactDataBase.Pages.Account;
using ContactDataBase.Pages.Contact;
using EdgeDB;

namespace ContactDataBase
{
    public class Query
    {
        private readonly EdgeDBClient _client;

        public Query(EdgeDBClient client)
        {
            _client = client;
        }

        public async Task InsertContactInfoInput(ContactInfoInput contactInfoInput)
        {
            string formattedDateOfBirth = contactInfoInput.DateOfBirth.ToString("yyyy-MM-dd");
            var query = @"
                INSERT ContactInfo {
                    first_name := <str>$first_name,
                    last_name := <str>$last_name,
                    email := <str>$email,
                    username := <str>$username,
                    password := <str>$password,
                    title := <str>$title,
                    description := <str>$description,
                    date_birth := cal::to_local_date(<str>$date_birth),
                    marriage_status := <bool>$marriage_status,
                    role_user := <str>$role_user
                }";
            await _client.ExecuteAsync(query, new Dictionary<string, object?>
            {
                {"first_name", contactInfoInput.FirstName},
                {"last_name", contactInfoInput.LastName},
                {"email", contactInfoInput.Email },
                {"username", contactInfoInput.Username},
                {"password", contactInfoInput.Password},
                {"title", contactInfoInput.Title},
                {"description", contactInfoInput.Description},
                {"date_birth", formattedDateOfBirth},
                {"marriage_status", contactInfoInput.MarriageStatus},
                {"role_user", contactInfoInput.RoleUser}
            });
        }

        public async Task UpdateContactInfoInputWithId(string id, ContactInfoInput contactInfoInput)
        {
            string formattedDateOfBirth = contactInfoInput.DateOfBirth.ToString("yyyy-MM-dd");
            var query = @"
                update ContactInfo 
                filter .id = <uuid><str>$id
                set {
                    first_name := <str>$first_name,
                    last_name := <str>$last_name,
                    email := <str>$email,
                    username := <str>$username,
                    password := <str>$password,
                    title := <str>$title,
                    description := <str>$description,
                    date_birth := cal::to_local_date(<str>$date_birth),
                    marriage_status := <bool>$marriage_status,
                    role_user := <str>$role_user
                }";
            await _client.ExecuteAsync(query, new Dictionary<string, object?>
            {
                {"id", id},
                {"first_name", contactInfoInput.FirstName},
                {"last_name", contactInfoInput.LastName},
                {"email", contactInfoInput.Email },
                {"username", contactInfoInput.Username},
                {"password", contactInfoInput.Password},
                {"title", contactInfoInput.Title},
                {"description", contactInfoInput.Description},
                {"date_birth", formattedDateOfBirth},
                {"marriage_status", contactInfoInput.MarriageStatus},
                {"role_user", contactInfoInput.RoleUser}
            });
        }

        public async Task<ContactInfoInput> GetContactWithId(string id)
        {
            ContactInfo? currentContactInfo = await _client.QuerySingleAsync<ContactInfo>($@"
                SELECT ContactInfo {
                    first_name,
                    last_name,
                    email,
                    username,
                    password,
                    title,
                    description,
                    date_birth,
                    marriage_status,
                    role_user
                }
                filter .id = <uuid>""{id}""
            ");

            return ContactInfoInput.FromContactInfo(currentContactInfo);
        }

        public async Task<ContactInfo> GetContactWithUsername(string username)
        {
            return await _client.QuerySingleAsync<ContactInfo>($@"
                SELECT ContactInfo {
                    username,
                    password,
                    role_user
                }
                filter .username = ""{username}""
            ");
        }

        public async Task DeleteContactInfoInputWithId(string id)
        {
            await _client.QuerySingleAsync<ContactInfo>($@"
                delete ContactInfo
                filter .id = <uuid>""{id}""
            ");
        }

        public async Task<List<ContactInfo>> GetAllContactList()
        {
            return (await _client.QueryAsync<ContactInfo>(@"
                SELECT ContactInfo {
                    first_name,
                    last_name,
                    email,
                    username,
                    title,
                    description,
                    date_birth,
                    marriage_status,
                    role_user
                }
                ORDER BY .first_name
            ")).ToList();
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            var count = await _client.QuerySingleAsync<int>(
            $@"select count(
            (select ContactInfo
             filter ContactInfo.username like '{username}')
              );"
            );

            return count > 0;
        }
    }
}
