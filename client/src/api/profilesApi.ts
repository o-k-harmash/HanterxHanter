import { client } from "./client";
import { Profile, ProfilesApiResponse } from "./constants/profilesApiResponse";

export const BASE_URL = "http://localhost:5010/api/profile/filtered";

export async function fetchUserProfiles() {
  const queryParams = new URLSearchParams({
    genderId: "male",
    cityId: "new_york",
    minAge: "0",
    maxAge: "19",
    pageNum: "1",
    pageSize: "10",
  }).toString();

  const urlWithParams = `${BASE_URL}?${queryParams}`;

  try {
    const response = await client.get<ProfilesApiResponse>(urlWithParams);
    return response.data;
  } catch (error) {
    console.error("Error fetching user profile:", error);
    throw new Error();
  }
}

export async function fetchUserProfile(userId: number) {
  const urlWithParams = `${BASE_URL}/${userId}`;

  try {
    const response = await client.get<Profile>(urlWithParams);
    return response.data;
  } catch (error) {
    console.error("Error fetching user profile:", error);
    throw new Error();
  }
}
