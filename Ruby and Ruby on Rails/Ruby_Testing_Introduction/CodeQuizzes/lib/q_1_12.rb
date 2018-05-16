class String
  def alliteration?
    letter = self[0]
    # all? will return true only if none of the collection members are false or nil.
    self.split.all? do |f|
      f[0] == letter
    end
  end
end
