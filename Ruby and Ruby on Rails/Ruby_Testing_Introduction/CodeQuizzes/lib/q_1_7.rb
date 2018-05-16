class String
  def titlecase
    self.split.map{|i| i.capitalize}.join(' ')
  end
end
