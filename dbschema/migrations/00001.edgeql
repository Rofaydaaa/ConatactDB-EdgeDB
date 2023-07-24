CREATE MIGRATION m17zndolmla2f6fz3vzbq6xvnx63gzsnbzyghrc22w5qumzighslwq
    ONTO initial
{
  CREATE SCALAR TYPE default::Title EXTENDING enum<Mr, Mrs, Miss, Dr, Prof>;
  CREATE TYPE default::ContactInfo {
      CREATE REQUIRED PROPERTY date_of_birth: std::datetime;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY title: default::Title;
  };
};
