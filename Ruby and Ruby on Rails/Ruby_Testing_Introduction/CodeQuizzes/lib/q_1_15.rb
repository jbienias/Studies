class Integer
  def concat(num) #"#{self}#{num}".to_i
    str_self = self.to_s
    str_num = num.to_s
    str_result = str_self + str_num
    str_result.to_i
  end
end
