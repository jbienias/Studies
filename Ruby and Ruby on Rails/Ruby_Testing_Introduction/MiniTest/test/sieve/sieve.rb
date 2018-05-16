class Sieve
  def initialize(number)
    raise ArgumentError if number < 2
    @number = number
    @primes = (0..number).to_a
    @primes[0] = false
    @primes[1] = false
    for i in 2...number
      @primes[i] = true
    end
  end

 # https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
  def primes
    for i in 2...Math.sqrt(@number)
      if @primes[i]
        j = i ^ 2
        counter = 0
        while j < @number
          @primes[j] = false
          counter += 1
          j += counter * i
        end
      end
    end
    result = []
    for i in 0...@number
      result.push(@number[i]) if @prime[i] == true
    end
    p result
  end
end


module BookKeeping
  VERSION = 1
end
