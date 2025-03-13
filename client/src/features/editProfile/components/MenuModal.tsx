// src/features/editProfile/components/MenuModal.tsx

import Menu from "../../../components/common/Menu";
import {
  SelectionCheck,
  SelectionGroup,
  SelectionLabel,
  SelectionTitle,
} from "../../../components/common/SelectionInput";
import CSharedCard from "../../../components/CSharedCard";
import { initialState } from "../constants/editViewState";
import { Config, IFormState, ProfileState } from "../types";

interface MenuModalProps {
  formState: IFormState;
  setState: React.Dispatch<React.SetStateAction<ProfileState>>;
  state: ProfileState;
  inputs: Config;
}

export const MenuModal: React.FC<MenuModalProps> = ({
  setState,
  inputs,
  formState,
  state,
}: MenuModalProps) => {
  return (
    <Menu blurClick={() => setState(initialState)}>
      <CSharedCard title={state.stateName}>
        <SelectionGroup>
          {inputs
            .filter((e) => e.name === state.stateName)
            .map((inpt, indx) => (
              <SelectionLabel
                key={indx}
                {...inpt}
                checked={inpt.checked(formState)}
              >
                <SelectionTitle value={inpt.value}></SelectionTitle>
                <SelectionCheck></SelectionCheck>
              </SelectionLabel>
            ))}
        </SelectionGroup>
      </CSharedCard>
    </Menu>
  );
};
