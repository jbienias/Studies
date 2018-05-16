class Float
  def to_money
    sprintf("%.2f", self).prepend('$')
  end
end

#class Float
#  def to_money
#    "$" + self.round(2).to_s
#  end
#end
