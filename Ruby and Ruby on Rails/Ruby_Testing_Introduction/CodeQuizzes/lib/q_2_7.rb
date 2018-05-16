class Array
  def tight_zip(array)
    self.zip(array).flatten.compact
  end
end

#'zip' interleaves the element of its receiver with those of its argument, using nil to pad if the first array is longer than the second; that is, [:a,:b,:c].zip([1,2])==[[:a,1],[:b,2],[:c,nil]]
#'flatten' takes an array that includes arbitrarily nested arrays and flattens them into a single array with no nested arrays, that is, [[:a,1],[:b,2],[:c,nil]].flatten==[:a,1,:b,2,:c,nil]
#'compact' removes nil elements from a collection, that is, [:a,1,:c,nil].compact==[:a,1,:c]
