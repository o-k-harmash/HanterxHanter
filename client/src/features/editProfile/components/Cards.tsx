// src/features/editProfile/components/Cards.tsx
import CCard from "../../../components/CCard";
import { IFormState } from "../types";

interface CardsProps {
  cards: any[];
  formState: IFormState;
  onClick: (name: string) => void;
}

export const Cards: React.FC<CardsProps> = ({ cards, formState, onClick }) => {
  return (
    <>
      {cards.map((key, indx) => (
        <CCard key={indx} title={key.name} onClick={() => onClick(key.name)}>
          <span>{key.values(formState).join(",")}</span>
        </CCard>
      ))}
    </>
  );
};
