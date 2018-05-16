class Hamming
  def self.compute(x, y)
    if x.length != y.length
      raise ArgumentError
    end
    errors = 0
    i = 0
    while i < x.length
      if x[i] != y[i]
        errors += 1
      end
      i += 1
    end
    errors
  end
end
