import React, { useState } from "react";
import CCard from "../../components/CCard";

import MediaInput from "../../components/common/MediaInput";
import CSharedCard from "../../components/CSharedCard";

import Navbar from "../../layouts/Navbar";
import {
  EditProfileState,
  initialState,
  interestsState,
  languagesState,
  relationshipGoalsState,
  sexualOrientationState,
} from "./constants/editViewState";

import Menu from "../../components/common/Menu";
import {
  SelectionElement,
  SelectionInput,
} from "../../components/common/SelectionInput";
import {
  interests,
  languages,
  relationshipGoalsValues,
  sexualOrientationValues,
} from "./constants/editData";
import {
  useMultipleSelection,
  useSingleSelection,
} from "../../hooks/useSelection/useSelection";
import useFile from "../../hooks/useFile/useFile";
import Button from "../../components/Button";

function EditProfile() {
  const [state, setState] = useState<EditProfileState>(initialState);
  const sexualOrientationSelectionState = useSingleSelection<SelectionElement>(
    sexualOrientationValues[0]
  );
  const files = {
    mainFile: useFile(),
    subFileFirst: useFile(),
    subFileSecond: useFile(),
  };
  const languageSelectionState = useMultipleSelection<SelectionElement>(
    languages.slice(1, 4)
  );
  const interestsSelectionState = useMultipleSelection<SelectionElement>(
    interests.slice(1, 2)
  );
  const relationshipGoalsSelectionState = useSingleSelection<SelectionElement>(
    relationshipGoalsValues[2]
  );

  let selectionElement = null;

  const onSubmit = () => {
    console.log({
      files,
      languageSelectionState,
      interestsSelectionState,
      relationshipGoalsSelectionState,
    });
  };

  if (state.isSexualOrientationOpen) {
    selectionElement = (
      <CSharedCard
        onClose={() => setState(initialState)}
        title={"Sexual Orientation"}
      >
        <SelectionInput
          {...sexualOrientationSelectionState}
          values={sexualOrientationValues}
        />
      </CSharedCard>
    );
  } else if (state.isLanguagesOrientationOpen) {
    selectionElement = (
      <CSharedCard onClose={() => setState(initialState)} title={"Languages"}>
        <SelectionInput {...languageSelectionState} values={languages} />
      </CSharedCard>
    );
  } else if (state.isInterestsOpen) {
    selectionElement = (
      <CSharedCard onClose={() => setState(initialState)} title={"Interests"}>
        <SelectionInput {...interestsSelectionState} values={interests} />
      </CSharedCard>
    );
  } else if (state.isRelationshipGoalOpen) {
    selectionElement = (
      <CSharedCard
        onClose={() => setState(initialState)}
        title={"Relationship Goal"}
      >
        <SelectionInput
          {...relationshipGoalsSelectionState}
          values={relationshipGoalsValues}
        />
      </CSharedCard>
    );
  }

  return (
    <>
      <Navbar></Navbar>
      <div className="edit">
        {state.isBlure && (
          <Menu blurClick={() => setState(initialState)}>
            {selectionElement || <></>}
          </Menu>
        )}
        <div className="edit__container">
          <div className="media-group">
            <div className="media-group__left-column">
              <MediaInput {...files.mainFile} />
            </div>
            <div className="media-group__right-column">
              <MediaInput {...files.subFileFirst} />
              <MediaInput {...files.subFileSecond} />
            </div>
          </div>
          <CCard
            title={"Sexual Orientation"}
            onClick={() => setState(sexualOrientationState)}
          >
            <span>{sexualOrientationSelectionState.selected?.value}</span>
          </CCard>
          <CCard title={"Languages"} onClick={() => setState(languagesState)}>
            <span>
              {languageSelectionState.selected.map((v) => v.value).join(", ")}
            </span>
          </CCard>
          <CCard title={"Interests"} onClick={() => setState(interestsState)}>
            <span>
              {interestsSelectionState.selected.map((v) => v.value).join(", ")}
            </span>
          </CCard>
          <CCard
            title={"Relationalship Goal"}
            onClick={() => setState(relationshipGoalsState)}
          >
            <span>{relationshipGoalsSelectionState.selected?.value}</span>
          </CCard>
          <Button onClick={onSubmit}>Update Profile</Button>
        </div>
      </div>
    </>
  );
}

export default EditProfile;
