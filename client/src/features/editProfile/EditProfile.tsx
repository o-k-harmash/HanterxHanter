// src/features/editProfile/EditProfile.tsx
import { useState } from "react";

import Navbar from "../../layouts/Navbar";
import { useFormState } from "./hooks/useFormState";
import {
  initialState,
  interestsState,
  languagesState,
  relationshipGoalsState,
  sexualOrientationState,
} from "./constants/editViewState";
import { FormInputs } from "./components/FormInputs";
import { cards, inputs } from "./constants/editData";
import { Cards } from "./components/Cards";
import { MenuModal } from "./components/MenuModal";
import MediaInput from "../../components/common/MediaInput";
import useFile from "../../hooks/useFile/useFile";

const EditProfile = () => {
  const { formState, getInputChangeHandler } = useFormState();
  const [state, setState] = useState(initialState);
  const files = {
    mainFile: useFile(),
    subFileFirst: useFile(),
    subFileSecond: useFile(),
  };

  const stateFabric = (state: string) => {
    switch (state) {
      case "languages":
        setState(languagesState);
        break;
      case "interests":
        setState(interestsState);
        break;
      case "orientation":
        setState(sexualOrientationState);
        break;
      case "goal":
        setState(relationshipGoalsState);
        break;
      default:
        setState(initialState);
    }
  };

  const onSubmit = (e: any) => {
    e.preventDefault();
    console.log(e.target.elements);
  };

  return (
    <>
      <Navbar />
      {state.isBlure && (
        <MenuModal
          formState={formState}
          setState={setState}
          state={state}
          inputs={inputs}
        />
      )}
      <div className="edit">
        <div className="edit__container">
          <form onSubmit={onSubmit}>
            <div className="media-group">
              <div className="media-group__left-column">
                <MediaInput {...files.mainFile} />
              </div>
              <div className="media-group__right-column">
                <MediaInput {...files.subFileFirst} />
                <MediaInput {...files.subFileSecond} />
              </div>
            </div>
            <FormInputs
              formState={formState}
              inputs={inputs}
              getInputChangeHandler={getInputChangeHandler}
            />
            <button type="submit">Save</button>
          </form>
          <Cards cards={cards} formState={formState} onClick={stateFabric} />
        </div>
      </div>
    </>
  );
};

export default EditProfile;
