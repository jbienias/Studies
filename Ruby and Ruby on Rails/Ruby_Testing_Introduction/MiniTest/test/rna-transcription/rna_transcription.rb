# Comment to tell linter to shut up...
class Complement
  DNA_PAIRINGS = { 'G' => 'C', 'C' => 'G', 'A' => 'U', 'T' => 'A' }.freeze
  def self.of_dna(str)
    str = str.upcase
    # The =~ operator matches the regular expression against a string, and it returns either the offset of the match from the string if it is found, otherwise nil.
    # https://stackoverflow.com/questions/5781362/ruby-operator
    if str =~ /[^GCAT]/
      ''
    else
      result = str.chars.map do |i|
        DNA_PAIRINGS[i]
      end
      result.join # result.join("") / join('')
    end
  end
end

module BookKeeping
  VERSION = 4
end
