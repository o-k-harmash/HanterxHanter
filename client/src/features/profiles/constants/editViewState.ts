export interface EditProfileState {
  isBlure: boolean;
  isRelationshipGoalOpen: boolean;
  isSexualOrientationOpen: boolean;
  isLanguagesOrientationOpen: boolean;
  isInterestsOpen: boolean;
}

export const relationshipGoalsState: EditProfileState = {
  isBlure: true,
  isRelationshipGoalOpen: true,
  isSexualOrientationOpen: false,
  isLanguagesOrientationOpen: false,
  isInterestsOpen: false,
};

export const sexualOrientationState: EditProfileState = {
  isBlure: true,
  isRelationshipGoalOpen: false,
  isSexualOrientationOpen: true,
  isLanguagesOrientationOpen: false,
  isInterestsOpen: false,
};

export const languagesState: EditProfileState = {
  isBlure: true,
  isRelationshipGoalOpen: false,
  isSexualOrientationOpen: false,
  isLanguagesOrientationOpen: true,
  isInterestsOpen: false,
};

export const interestsState: EditProfileState = {
  isBlure: true,
  isRelationshipGoalOpen: false,
  isSexualOrientationOpen: false,
  isLanguagesOrientationOpen: false,
  isInterestsOpen: true,
};

export const initialState: EditProfileState = {
  isBlure: false,
  isRelationshipGoalOpen: false,
  isSexualOrientationOpen: false,
  isLanguagesOrientationOpen: false,
  isInterestsOpen: false,
};
