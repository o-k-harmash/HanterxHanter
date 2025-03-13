export interface User {
  userId: string;
  firstName: string;
  lastName: string;
  birthDay: Date;
  genderId: string;
  address: {
    countryId: string;
    cityId: string;
    stateId: string;
  };
  age: number;
  avatar: string;
}
