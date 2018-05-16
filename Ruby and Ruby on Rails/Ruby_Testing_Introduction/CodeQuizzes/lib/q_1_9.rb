require 'date'

class String
  def date_convertible?
    !!(Date.parse(self) rescue false)
  end
end
