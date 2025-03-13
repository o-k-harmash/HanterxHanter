export type HobbyType =
  | "Reading"
  | "Cooking"
  | "Traveling"
  | "Painting"
  | "Cycling"
  | "Gaming"
  | "Music"
  | "Photography"
  | "Writing"
  | "Sports"
  | "Hiking"
  | "Gardening"
  | "Fishing"
  | "Yoga"
  | "Meditation"
  | "Dancing"
  | "Martial Arts"
  | "Drawing"
  | "Knitting"
  | "Coding"
  | "Bird Watching"
  | "Volunteer Work"
  | "Fisting"
  | "Scrapbooking"
  | "Origami"
  | "Wine Tasting"
  | "Astronomy"
  | "Chess"
  | "Magic"
  | "Pottery"
  | "Calligraphy"
  | "Homebrewing"
  | "Stand-up Comedy"
  | "Blogging"
  | "Vlogging"
  | "DIY (Do It Yourself)"
  | "Camping"
  | "Woodworking"
  | "Sewing"
  | "Playing Instruments"
  | "Podcasting";

export interface Profile {
  profileId: number;
  name: string;
  age: number;
  bio: string;
  gender: string;
  fileStrings: string[];
  cityString: string;
  interestStrings: string[];
  languagesStrings: string[];
}

export interface ProfilesApiResponse {
  filterPageData: {
    maxAge: number;
    minAge: number;
    cityId: string;
    genderId: string;
    pageNum: number;
    pageSize: number;
    numPages: number;
  };
  profilesList: Profile[];
}
