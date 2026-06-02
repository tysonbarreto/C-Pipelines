import { createContext, useContext } from "react";

//create context object
export const PersonContext = createContext(null);

//custom hook to use the context
export const usePersonContext = () => {
  const ctx = useContext(PersonContext);
  if (!ctx)
    throw new Error("usePersonContext must be used inside PersonProvider");
  return ctx;
};
