import React from "react";

import "./surface.css";

type SurfaceProps = {
  style?: React.CSSProperties;
  shadowDepth?: number;
  children?: React.ReactNode;
};

const Surface = ({ style, shadowDepth = 3, children }: SurfaceProps) => {
  // Функция для генерации тени в зависимости от глубины
  const generateShadow = (depth: number) => {
    const intensity = depth * 2; // Умножаем глубину на 2 для интенсивности тени
    return `${intensity}px ${intensity}px ${
      intensity * 2
    }px rgba(0, 0, 0, 0.3)`;
  };

  // Кастомный стиль с применением тени
  const customStyle = {
    ...style, // Пользовательские стили
    boxShadow: generateShadow(shadowDepth), // Добавляем тень на основе глубины
  };

  return (
    <div className="surface" style={customStyle}>
      {children}
    </div>
  );
};

export default Surface;
