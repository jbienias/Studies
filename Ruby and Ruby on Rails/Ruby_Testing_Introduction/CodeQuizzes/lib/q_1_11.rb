class String
  def acronym?(acronym)
    first_letters = self.split.map {|f| f[0]}.join
    first_letters == acronym
  end
end
