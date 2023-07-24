CREATE MIGRATION m1hsgdqqv5f5plgebkpmlzi6b3bnddaw4ngeinpgkejvw5jctltvkq
    ONTO m1kvs53gm5jnnzjhjnknhzzxhe4nsdjuq57nkbnwhdfvcltpe5jydq
{
  ALTER TYPE default::ContactInfo {
      CREATE REQUIRED PROPERTY date_birth: cal::local_date {
          SET REQUIRED USING (<cal::local_date>{});
      };
  };
  ALTER TYPE default::ContactInfo {
      DROP PROPERTY date_of_birth;
  };
};
