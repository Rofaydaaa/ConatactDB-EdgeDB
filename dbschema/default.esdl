module default {
    scalar type Title extending enum<Mr, Mrs, Miss, Dr, Prof>;
    scalar type RoleUser extending enum<Admin, Normal>;
    type ContactInfo {
        required first_name: str;
        required last_name: str;
        required email: str;
        required username: str {
            constraint exclusive;
        };
        required password: str;
        required title: Title;
        required description: str;
        required date_birth: cal::local_date;
        required marriage_status: bool;
        required role_user: RoleUser;
    }
}
