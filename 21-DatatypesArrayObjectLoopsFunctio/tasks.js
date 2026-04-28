
function removeRepeatedDigits(numbers) {
  const uniqueNumbers = [];
  let repeatedCount = 0;

  for (let i = 0; i < numbers.length; i++) {
    if (uniqueNumbers.includes(numbers[i])) {
      repeatedCount++;
    } else {
      uniqueNumbers.push(numbers[i]);
    }
  }

  return {
    uniqueNumbers,
    repeatedCount,
  };
}

const digits = [1, 2, 3, 2, 4, 5, 1, 3, 6];
const digitResult = removeRepeatedDigits(digits);
console.log("Array without repeated digits:", digitResult.uniqueNumbers);
console.log("Number of repeated digits:", digitResult.repeatedCount);


function isPalindrome(word) {
  const reversedWord = word.split("").reverse().join("");
  return word === reversedWord;
}

const word = "level";
console.log(`${word} is palindrome:`, isPalindrome(word));


function countElementsGreaterThanNumber(numbers, enteredNumber) {
  let count = 0;

  for (let i = 0; i < numbers.length; i++) {
    if (enteredNumber < numbers[i]) {
      count++;
    }
  }

  return count;
}

const numbers = [4, 9, 2, 15, 7, 1];
const enteredNumber = 6;
console.log(
  `There are ${countElementsGreaterThanNumber(numbers, enteredNumber)} elements greater than ${enteredNumber}.`
);

function checkAbundantOrDeficient(number) {
  let divisorsSum = 0;

  for (let i = 1; i < number; i++) {
    if (number % i === 0) {
      divisorsSum += i;
    }
  }

  if (divisorsSum > number) {
    return "Abundant";
  }

  return "Deficient";
}

console.log("12 is", checkAbundantOrDeficient(12));
console.log("13 is", checkAbundantOrDeficient(13));


function squareArray(numbers) {
  const squaredNumbers = [];

  for (let i = 0; i < numbers.length; i++) {
    squaredNumbers.push(numbers[i] * numbers[i]);
  }

  return squaredNumbers;
}

console.log("Squared array:", squareArray([2, 3, 4, 5]));

if (typeof document !== "undefined") {
  document.getElementById("repeated-digits").textContent =
    `Given array: [${digits.join(", ")}]
Array without repeated digits: [${digitResult.uniqueNumbers.join(", ")}]
Number of repeated digits: ${digitResult.repeatedCount}`;

  document.getElementById("palindrome").textContent =
    `${word} is ${isPalindrome(word) ? "a palindrome" : "not a palindrome"}.`;

  document.getElementById("smaller-than").textContent =
    `Array: [${numbers.join(", ")}]
Entered number: ${enteredNumber}
${countElementsGreaterThanNumber(numbers, enteredNumber)} elements are greater than ${enteredNumber}.`;

  document.getElementById("abundant-deficient").textContent =
    `12 is ${checkAbundantOrDeficient(12)}
13 is ${checkAbundantOrDeficient(13)}`;

  document.getElementById("squared-array").textContent =
    `Original array: [2, 3, 4, 5]
Squared array: [${squareArray([2, 3, 4, 5]).join(", ")}]`;
}
