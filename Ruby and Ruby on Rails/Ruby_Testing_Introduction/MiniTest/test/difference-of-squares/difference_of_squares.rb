class Squares
  def initialize(number)
    raise AttributeError if number.to_i.zero? ## number.to_i == 0, https://apidock.com/ruby/String/to_i
    @number = number
  end

  def square_of_sum
    i = 1
    result = 0
    while i <= @number
      result += i
      i += 1
    end
    result**2
  end

  def sum_of_squares
    i = 1
    result = 0
    while i <= @number
      result += i**2
      i += 1
    end
    result
  end

  def difference
    a = sum_of_squares
    b = square_of_sum
    if a > b
      a - b
    else
      b - a
    end
  end
end

module BookKeeping
  VERSION = 4
end
