import { Config, IFormState } from "../types";

export const inputs: Config = [
  {
    type: "checkbox",
    name: "interests",
    value: "book",
    checked: (state: IFormState) => state["interests"]["book"],
    id: "interests_book",
  },
  {
    type: "checkbox",
    name: "interests",
    value: "sport",
    checked: (state: IFormState) => state["interests"]["sport"],
    id: "interests_sport",
  },
  {
    type: "checkbox",
    name: "languages",
    value: "russian",
    checked: (state: IFormState) => state["languages"]["russian"],
    id: "languages_russian",
  },
  {
    type: "checkbox",
    name: "languages",
    value: "english",
    checked: (state: IFormState) => state["languages"]["english"],
    id: "languages_english",
  },
  {
    type: "radio",
    name: "goal",
    value: "ara ara",
    checked: (state: IFormState) => state["goal"] === "ara ara",
    id: "goal_ara_ara",
  },
  {
    type: "radio",
    name: "goal",
    value: "bib bib",
    checked: (state: IFormState) => state["goal"] === "bib bib",
    id: "goal_bib_bib",
  },
  {
    type: "radio",
    name: "orientation",
    value: "trapek",
    checked: (state: IFormState) => state["orientation"] === "trapek",
    id: "orientation_trapek",
  },
  {
    type: "radio",
    name: "orientation",
    value: "normal",
    checked: (state: IFormState) => state["orientation"] === "normal",
    id: "orientation_normal",
  },
  {
    type: "radio",
    name: "orientation",
    value: "other",
    checked: (state: IFormState) => state["orientation"] === "other",
    id: "orientation_other",
  },
];

export const cards = [
  {
    name: "languages",
    values: (state: IFormState) =>
      Object.keys(state["languages"]).filter(
        (key: string) => state["languages"][key]
      ),
  },
  {
    name: "interests",
    values: (state: IFormState) =>
      Object.keys(state["interests"]).filter(
        (key: string) => state["interests"][key]
      ),
  },
  {
    name: "goal",
    values: (state: IFormState) => [state["goal"]],
  },
  {
    name: "orientation",
    values: (state: IFormState) => [state["orientation"]],
  },
];
