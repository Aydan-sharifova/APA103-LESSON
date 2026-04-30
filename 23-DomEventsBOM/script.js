const root = document.querySelector("#calculator-root");

const keys = [
  { label: "AC", type: "function", action: "clear" },
  { label: "+/-", type: "function", action: "sign" },
  { label: "%", type: "function", action: "percent" },
  { label: "÷", type: "operator", action: "operator", value: "/" },
  { label: "7", action: "number", value: "7" },
  { label: "8", action: "number", value: "8" },
  { label: "9", action: "number", value: "9" },
  { label: "×", type: "operator", action: "operator", value: "*" },
  { label: "4", action: "number", value: "4" },
  { label: "5", action: "number", value: "5" },
  { label: "6", action: "number", value: "6" },
  { label: "-", type: "operator", action: "operator", value: "-" },
  { label: "1", action: "number", value: "1" },
  { label: "2", action: "number", value: "2" },
  { label: "3", action: "number", value: "3" },
  { label: "+", type: "operator", action: "operator", value: "+" },
  { label: "0", className: "zero", action: "number", value: "0" },
  { label: ".", action: "decimal" },
  { label: "=", type: "operator", action: "equals" }
];

let currentValue = "0";
let storedValue = null;
let pendingOperator = null;
let shouldResetDisplay = false;
let hasError = false;

function createCalculator() {
  const phone = document.createElement("section");
  phone.className = "phone";
  phone.setAttribute("aria-label", "Calculator");

  const display = document.createElement("output");
  display.className = "display";
  display.textContent = currentValue;
  display.setAttribute("aria-live", "polite");

  const keypad = document.createElement("div");
  keypad.className = "keypad";

  keys.forEach((key) => {
    const button = document.createElement("button");
    button.type = "button";
    button.className = ["key", key.type, key.className].filter(Boolean).join(" ");
    button.textContent = key.label;
    button.dataset.action = key.action;

    if (key.value) {
      button.dataset.value = key.value;
    }

    button.addEventListener("click", () => handleInput(key));
    keypad.append(button);
  });

  const homeLine = document.createElement("div");
  homeLine.className = "home-line";

  phone.append(display, keypad, homeLine);
  root.append(phone);
}

function updateDisplay() {
  const display = document.querySelector(".display");
  display.textContent = formatDisplay(currentValue);
}

function formatDisplay(value) {
  if (value === "Error") {
    return value;
  }

  if (value.length <= 10) {
    return value;
  }

  const numberValue = Number(value);

  if (!Number.isFinite(numberValue)) {
    return "Error";
  }

  return numberValue.toExponential(5).replace("+", "");
}

function resetCalculator() {
  currentValue = "0";
  storedValue = null;
  pendingOperator = null;
  shouldResetDisplay = false;
  hasError = false;
}

function handleInput(key) {
  if (hasError && key.action !== "clear") {
    resetCalculator();
  }

  if (key.action === "number") {
    addNumber(key.value);
  }

  if (key.action === "decimal") {
    addDecimal();
  }

  if (key.action === "operator") {
    chooseOperator(key.value);
  }

  if (key.action === "equals") {
    calculateResult();
  }

  if (key.action === "clear") {
    resetCalculator();
  }

  if (key.action === "sign") {
    toggleSign();
  }

  if (key.action === "percent") {
    convertToPercent();
  }

  updateDisplay();
}

function addNumber(number) {
  if (shouldResetDisplay) {
    currentValue = number;
    shouldResetDisplay = false;
    return;
  }

  if (currentValue === "0") {
    currentValue = number;
    return;
  }

  if (currentValue.replace("-", "").replace(".", "").length < 9) {
    currentValue += number;
  }
}

function addDecimal() {
  if (shouldResetDisplay) {
    currentValue = "0.";
    shouldResetDisplay = false;
    return;
  }

  if (!currentValue.includes(".")) {
    currentValue += ".";
  }
}

function chooseOperator(operator) {
  if (pendingOperator && !shouldResetDisplay) {
    calculateResult();
  }

  storedValue = Number(currentValue);
  pendingOperator = operator;
  shouldResetDisplay = true;
}

function calculateResult() {
  if (!pendingOperator || storedValue === null) {
    return;
  }

  const nextValue = Number(currentValue);
  const result = operate(storedValue, nextValue, pendingOperator);

  if (!Number.isFinite(result)) {
    currentValue = "Error";
    storedValue = null;
    pendingOperator = null;
    shouldResetDisplay = true;
    hasError = true;
    return;
  }

  currentValue = cleanNumber(result);
  storedValue = null;
  pendingOperator = null;
  shouldResetDisplay = true;
}

function operate(firstNumber, secondNumber, operator) {
  if (operator === "+") {
    return firstNumber + secondNumber;
  }

  if (operator === "-") {
    return firstNumber - secondNumber;
  }

  if (operator === "*") {
    return firstNumber * secondNumber;
  }

  if (operator === "/") {
    return secondNumber === 0 ? NaN : firstNumber / secondNumber;
  }

  return secondNumber;
}

function cleanNumber(number) {
  const rounded = Number.parseFloat(number.toPrecision(12));
  return String(rounded);
}

function toggleSign() {
  if (currentValue === "0" || currentValue === "Error") {
    return;
  }

  currentValue = currentValue.startsWith("-")
    ? currentValue.slice(1)
    : `-${currentValue}`;
}

function convertToPercent() {
  if (currentValue === "Error") {
    return;
  }

  currentValue = cleanNumber(Number(currentValue) / 100);
}

document.addEventListener("keydown", (event) => {
  const keyMap = {
    Escape: { action: "clear" },
    Backspace: { action: "clear" },
    Enter: { action: "equals" },
    "=": { action: "equals" },
    "+": { action: "operator", value: "+" },
    "-": { action: "operator", value: "-" },
    "*": { action: "operator", value: "*" },
    "/": { action: "operator", value: "/" },
    ".": { action: "decimal" },
    "%": { action: "percent" }
  };

  if (/^\d$/.test(event.key)) {
    handleInput({ action: "number", value: event.key });
    return;
  }

  if (keyMap[event.key]) {
    event.preventDefault();
    handleInput(keyMap[event.key]);
  }
});

createCalculator();
