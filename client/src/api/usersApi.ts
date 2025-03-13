import { client } from "./client";
import { User } from "./constants/usersApiResponse";

export const BASE_URL = "http://localhost:5010/api/user";

export async function fetchUser() {
  const urlWithParams = `${BASE_URL}/f86f8811-0a51-48fb-8cf8-c7c2f19d045a`;

  try {
    const response = await client.get<User>(urlWithParams);
    return response.data;
  } catch (error) {
    console.error("Error fetching user:", error);
    throw new Error();
  }
}
