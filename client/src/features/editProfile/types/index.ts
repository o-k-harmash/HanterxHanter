// src/features/editProfile/types/index.ts
export interface ICheckGroup {
  [key: string]: boolean;
}

export interface IFormState {
  files: {
    file1?: File;
    file2?: File;
    file3?: File;
  };
  goal: string;
  orientation: string;
  interests: ICheckGroup;
  languages: ICheckGroup;
}

export type Config = {
  type: "checkbox" | "radio" | "file";
  name: keyof IFormState;
  value: string;
  checked: (state: IFormState) => boolean;
  id: string;
}[];

export interface ProfileState {
  stateName: string;
  isBlure: boolean;
}
