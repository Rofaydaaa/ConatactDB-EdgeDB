CREATE MIGRATION m1hovfwf6dchtf65lqzkkz3am32tqzysrytxogovim6dpeqdcwigsq
    ONTO m1hsgdqqv5f5plgebkpmlzi6b3bnddaw4ngeinpgkejvw5jctltvkq
{
  ALTER TYPE default::ContactInfo {
      CREATE REQUIRED PROPERTY password: std::str {
          SET REQUIRED USING ('admin');
      };
  };
  CREATE SCALAR TYPE default::RoleUser EXTENDING enum<Admin, Normal>;
  ALTER TYPE default::ContactInfo {
      CREATE REQUIRED PROPERTY role_user: default::RoleUser {
          SET REQUIRED USING ('Normal');
      };
      CREATE REQUIRED PROPERTY username: std::str {
          SET REQUIRED USING ('admin');
      };
  };
};
