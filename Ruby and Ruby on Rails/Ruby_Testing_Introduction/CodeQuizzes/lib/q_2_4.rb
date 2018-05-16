class Array
  def unzip
    self.inject([[], []]) do |m, (l, n)|
      m[0] << n
      m[1] << l
      m
    end
  end
end
