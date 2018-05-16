class Complement
  DNA_PAIRINGS = { 'G' => 'C', 'C' => 'G', 'A' => 'U', 'T' => 'A' }.freeze
  def self.of_dna(str)
    if str =~ /[^GCAT]/
      ''
    else
      str = str.upcase
      result = str.chars.map do |i|
        DNA_PAIRINGS[i]
      end
      result.join
    end
  end
end
