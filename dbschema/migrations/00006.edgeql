CREATE MIGRATION m1z7opqhlqbowunjslupdyoa6ab6yafv26tli3qjmaj223fjyidspa
    ONTO m1hovfwf6dchtf65lqzkkz3am32tqzysrytxogovim6dpeqdcwigsq
{
  ALTER TYPE default::ContactInfo {
      ALTER PROPERTY username {
          CREATE CONSTRAINT std::exclusive;
      };
  };
};
