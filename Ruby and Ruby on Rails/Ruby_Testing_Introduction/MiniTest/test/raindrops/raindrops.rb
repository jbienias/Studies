# comment to tell linter rubocop to shut up
# linter rubocop faworyzuje (number % 5).zero?
class Raindrops
  def self.convert(number)
    str = ''
    # linter rubocop faworyzuje : '(number % 3).zero?'
    # rowniez jednolinijkowe if'y : str += 'Pling' if (number % 3).zero?
    if number % 3 == 0
      str += 'Pling'
    end
    if number % 5 == 0
      str += 'Plang'
    end
    if number % 7 == 0
      str += 'Plong'
    end
    if str == '' # str.empty?
      number.to_s
    else
      str
    end
  end
end

#p Raindrops.convert(3)
#p Raindrops.convert(4)
#p Raindrops.convert(21)
#p Raindrops.convert(105)
#p Raindrops.convert(5)

module BookKeeping
  VERSION = 3
end
