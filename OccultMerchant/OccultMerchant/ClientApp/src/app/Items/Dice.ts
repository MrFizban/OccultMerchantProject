export  class Dice {
  public number: number;
  public value: number;

  constructor(_number: number = 1, _value: number = 1) {
    this.number = _number;
    this.value = _value;
  }

  toString(): string {
    return this.number + "d" + this.value;
  }
}
