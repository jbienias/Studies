class Array
  def hashify
    #https://stackoverflow.com/questions/14528560/convert-an-array-to-hash-where-keys-are-the-indices
    Hash[*self] #creates hash from array
  end
end
