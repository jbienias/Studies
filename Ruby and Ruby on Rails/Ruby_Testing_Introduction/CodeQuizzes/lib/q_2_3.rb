class Array
  def mean
    result = 0
    self.each do |f|
      result += f
    end
    result.to_f / self.length
  end
end
