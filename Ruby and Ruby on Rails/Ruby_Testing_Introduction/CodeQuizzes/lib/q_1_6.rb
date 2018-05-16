class String
  def strip_whitespace
    self.gsub(/[ ]/, "") #gsub -> zamiana pierwszego znaku na drugi
  end
end
