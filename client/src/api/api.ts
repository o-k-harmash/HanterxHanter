import { client } from "./client";

export const BASE_URL = "http://localhost:5010/api";
export const BASE_PROFILE_URL = `${BASE_URL}/profile`;
export const BASE_USER_URL = `${BASE_URL}/user`;
export const BASE_FILE_URL = `${BASE_URL}/uploads`;
export const BASE_DROPDOWN_URL = `${BASE_URL}/dropdown`;

export interface Profile {
  profileId: number;
  name: string;
  age: number;
  bio: string;
  gender: string;
  fileStrings: string[];
  cityString: string;
  interestStrings: string[];
  languageStrings: string[];
}

export interface ProfileList {
  filterPageData: {
    maxAge: number;
    minAge: number;
    cityId: string;
    genderId: string;
    pageNum: number;
    pageSize: number;
    numPages: number;
    size: number;
    isEnd: boolean;
  };
  profileList: Profile[];
}

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

export interface Dropdown {
  [key: string]: {
    value: string;
    langCode: string;
  }[];
}

export interface DropdownList {
  [langCode: string]: {
    [key: string]: string;
  };
}

export async function fetchProfileList(
  searchParams?: string[][] | Record<string, string> | string | URLSearchParams
) {
  const queryParams = new URLSearchParams(searchParams).toString();
  const urlWithParams = `${BASE_PROFILE_URL}/filtered?${queryParams}`;
  try {
    const response = await client.get<ProfileList>(urlWithParams);
    return response.data;
  } catch (error) {
    console.error("Error fetching user profile:", error);
    throw new Error();
  }
}

export async function getProfile(userId: string) {
  const url = `${BASE_PROFILE_URL}/${userId}`;
  try {
    const response = await client.get<Profile>(url);
    return response.data;
  } catch (error) {
    console.error("Error fetching user profile:", error);
    throw new Error();
  }
}

export async function getUser(userId: string) {
  const url = `${BASE_USER_URL}/${userId}`;
  try {
    const response = await client.get<User>(url);
    return response.data;
  } catch (error) {
    console.error("Error fetching user:", error);
    throw new Error();
  }
}

export async function fetchDropdown(type: string) {
  const url = `${BASE_DROPDOWN_URL}/${type}/all`;
  try {
    const response = await client.get<Dropdown>(url);
    return response.data;
  } catch (error) {
    console.error("Error fetching dropdown data:", error);
    throw new Error();
  }
}

export async function fetchDropdownList() {
  const url = `${BASE_DROPDOWN_URL}/all`;
  try {
    const response = await client.get<DropdownList>(url);
    return response.data;
  } catch (error) {
    console.error("Error fetching dropdown data:", error);
    throw new Error();
  }
}
