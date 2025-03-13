import React from "react";

import DislikeIcon from "../icons/DislikeIcon";

import { classname } from "../../utils/styles";

import "./card.css";

type CardCloseProps = {
  onClose?: () => void; // Function to be called when the close button is clicked
};

/**
 * CardClose Component
 *
 * This component renders a close button (using `DislikeIcon`) and accepts an optional
 * `onClose` function which is triggered when the close button is clicked.
 *
 * @param {Object} props - The props for the CardClose component.
 * @param {Function} [props.onClose] - Optional callback function that is triggered when the close button is clicked.
 *
 * @example
 * <CardClose onClose={() => console.log("Card closed")} />
 */
export function CardClose({ onClose }: CardCloseProps) {
  return (
    <div className="c-card__close" onClick={onClose}>
      <DislikeIcon />
    </div>
  );
}

type CardHeaderProps = {
  style?: React.CSSProperties; // Custom styles for the header
  title: string; // Title for the header
  children?: React.ReactElement<typeof CardClose>; // CardClose component (optional)
};

/**
 * CardHeader Component
 *
 * This component represents the header of the card with a title and optionally
 * accepts a `CardClose` button as children to be displayed in the top-right corner.
 *
 * @param {Object} props - The props for the CardHeader component.
 * @param {React.CSSProperties} [props.style] - Optional custom CSS styles to be applied to the header.
 * @param {string} props.title - The title to be displayed in the card header.
 * @param {React.ReactElement} [props.children] - Optional `CardClose` component passed as a child.
 *
 * @example
 * <CardHeader title="Card Title"><CardClose onClose={() => alert("Close clicked!")} /></CardHeader>
 */
export function CardHeader({ style, title, children }: CardHeaderProps) {
  const customStyle: React.CSSProperties = {
    ...style, // Apply custom styles to the header
  };

  return (
    <div className="c-card__header" style={customStyle}>
      <h6 className="c-card__title">{title}</h6>
      {children}
    </div>
  );
}

type CardBodyProps = {
  style?: React.CSSProperties; // Optional custom styles for the body
  children?: React.ReactNode; // Content for the body
};

/**
 * CardBody Component
 *
 * This component represents the body of the card. It renders any content passed
 * as `children` inside the body of the card.
 *
 * @param {Object} props - The props for the CardBody component.
 * @param {React.CSSProperties} [props.style] - Optional custom CSS styles to be applied to the body.
 * @param {React.ReactNode} [props.children] - Content to be rendered inside the body.
 *
 * @example
 * <CardBody><p>Card content goes here</p></CardBody>
 */
export function CardBody({ style, children }: CardBodyProps) {
  const customStyle: React.CSSProperties = {
    ...style, // Apply custom styles to the body
  };

  return (
    <div className="c-card__boby" style={customStyle}>
      {children}
    </div>
  );
}

type CardTypes = "c-card_shared"; // Type for card variations (like "c-card_shared")

type CardProps = {
  style?: React.CSSProperties; // Custom styles for the whole card
  children?: React.ReactNode; // Content for the card
  type?: CardTypes; // Card type for applying conditional styles
};

/**
 * Card Component
 *
 * This is the main component that combines the `CardHeader` and `CardBody` components
 * together. It accepts `style`, `title`, `children`, and `type` props to customize the card.
 *
 * @param {Object} props - The props for the Card component.
 * @param {React.CSSProperties} [props.style] - Custom CSS styles for the whole card.
 * @param {React.ReactNode} [props.children] - Content to be rendered in the card.
 * @param {string} [props.type] - Type of the card, e.g., `"c-card_shared"`, for conditional styling.
 *
 * @example
 * <Card type="c-card_shared">
 *   <CardHeader title="Card Title">
 *     <CardClose onClose={() => console.log("Card closed")} />
 *   </CardHeader>
 *   <CardBody>
 *     <p>This is the body of the card</p>
 *   </CardBody>
 * </Card>
 */
function Card({ style, children, type }: CardProps) {
  const className = classname("c-card", type); // Applying conditional class names based on the card type

  const customStyle = {
    ...style, // Apply custom styles to the card
  };

  return (
    <div className={className} style={customStyle}>
      {children}
    </div>
  );
}

export default Card;
