export function classname(...classes: (string | unknown)[]): string {
  return classes.filter(String).join(" ");
}
