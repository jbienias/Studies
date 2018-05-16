def average_word_length(string)
  words = string.split(" ")
  if words.empty?
    return nil
  end
  words.inject(0) {|m, w| m += w.length}.to_f / words.length #to_f -> to float
end
