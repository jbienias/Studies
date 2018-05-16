def vowels(str)
  str.split.select do |f|
    f =~ /[aeiou]/
  end
end
