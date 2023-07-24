module default {
    scalar type Title extending enum<Mr, Mrs, Miss, Dr, Prof>;
    type ContactInfo {
        required first_name: str;
        required last_name: str;
        required email: str;
        required title: Title;
        required description: str;
        required date_birth: cal::local_date;
        required marriage_status: bool;
    }
}
