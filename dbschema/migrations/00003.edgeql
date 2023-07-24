CREATE MIGRATION m1kvs53gm5jnnzjhjnknhzzxhe4nsdjuq57nkbnwhdfvcltpe5jydq
    ONTO m1ubmf43moh7og74ctarcrvw2xblmozdjlf2dwzaqcafjecarwa6qa
{
  ALTER TYPE default::ContactInfo {
      CREATE REQUIRED PROPERTY date_of_birth: std::datetime {
          SET REQUIRED USING (<std::datetime>{});
      };
  };
};
