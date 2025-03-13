import { Config, IFormState } from "../types";

interface FormInputsProps {
  getInputChangeHandler: (
    type: "checkbox" | "radio" | "file"
  ) => (e: React.ChangeEvent<HTMLInputElement>) => void;
  formState: IFormState;
  inputs: Config;
}

export const FormInputs: React.FC<FormInputsProps> = ({
  formState,
  inputs,
  getInputChangeHandler,
}) => {
  return (
    <div style={{ display: "none" }}>
      {inputs.map((inpt, indx) => (
        <input
          key={indx}
          {...inpt}
          checked={inpt.checked(formState)}
          onChange={getInputChangeHandler(inpt.type)}
        />
      ))}
    </div>
  );
};
