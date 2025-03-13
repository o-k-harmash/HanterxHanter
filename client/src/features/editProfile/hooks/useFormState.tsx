// src/hooks/useFormState.ts
import { useState } from "react";
import { IFormState } from "../types";
import { initState } from "../constants/initFormState";

export function useFormState() {
  const [formState, setFormState] = useState<IFormState>(initState);

  const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormState((prevState: any) => ({
      ...prevState,
      [e.target.name]: {
        ...prevState[e.target.name],
        [e.target.value]: e.target.checked,
      },
    }));
  };

  const handleRadioChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormState((prevState) => ({
      ...prevState,
      [e.target.name]: e.target.value,
    }));
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormState((prevState) => ({
      ...prevState,
      [e.target.name]: e.target.files ? e.target.files[0] : undefined,
    }));
  };

  const getInputChangeHandler = (type: "checkbox" | "radio" | "file") => {
    switch (type) {
      case "checkbox":
        return handleCheckboxChange;
      case "radio":
        return handleRadioChange;
      case "file":
        return handleFileChange;
      default:
        return handleCheckboxChange; // default to checkbox if type is unknown
    }
  };

  return {
    formState,
    setFormState,
    handleCheckboxChange,
    handleRadioChange,
    handleFileChange,
    getInputChangeHandler,
  };
}
