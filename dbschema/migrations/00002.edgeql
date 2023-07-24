CREATE MIGRATION m1ubmf43moh7og74ctarcrvw2xblmozdjlf2dwzaqcafjecarwa6qa
    ONTO m17zndolmla2f6fz3vzbq6xvnx63gzsnbzyghrc22w5qumzighslwq
{
  ALTER TYPE default::ContactInfo {
      DROP PROPERTY date_of_birth;
  };
};
