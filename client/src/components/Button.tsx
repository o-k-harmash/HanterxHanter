type ButtonProps = {
  children: string;
  onClick?: (args: any) => void;
};

function Button({ onClick, children }: ButtonProps) {
  return (
    <button onClick={onClick} className="btn">
      {children}
    </button>
  );
}

export default Button;
