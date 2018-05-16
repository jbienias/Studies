class Pangram
  def self.pangram?(phrase)
    alphabet = ('a'..'z').to_a
    phrase.downcase.chars.each do |char|
      alphabet.delete(char)
    end
    if alphabet.empty?
      true
    else
      false
    end
  end

end


module BookKeeping
  VERSION = 5
end
