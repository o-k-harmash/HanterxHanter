import { IFormState } from "../types";

export const initState: IFormState = {
  files: {
    file1: undefined,
    file2: undefined,
    file3: undefined,
  },
  goal: "ara ara",
  orientation: "trapek",
  interests: {
    book: true,
    sport: false,
  },
  languages: {
    russian: true,
    english: false,
  },
};
